using PersonalApp.Business;
using PersonalApp.Core;
using PersonalApp.Entities;
using PersonalApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login();

            while (true)
            {
                Console.WriteLine("İşlem yapmak istediğiniz bölümü giriniz(1:Personel / 2:Departman / 3:İzin İşlemleri / 9:Çıkış)");

                string operation = Console.ReadLine();

                if (operation == "9")
                    Environment.Exit(0);
                else if (operation == "1")
                {
                    PersonOperation();
                }
                else if (operation == "2")
                {
                    DepartmentOperation();
                }
                else if (operation == "3")
                {
                    DayOffOperation();
                }
                else
                {
                    Console.WriteLine("Lütfen geçerli bir seçenek giriniz!");
                }
            }

        }
        /// <summary>
        /// Personel izinleri aksiyonu
        /// </summary>
        private static void DayOffOperation()
        {
            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz işlemi giriniz: 1:Güncelle / 2:İzni Yaklaşanları Listele / 5:Anasayfa / 9:Çıkış");
                var operation = Console.ReadLine();

                switch (operation)
                {
                    case "5":
                        return;
                    case "9":
                        Environment.Exit(0);
                        break;
                    case "1":
                        UpdateDayOffMenuView();
                        break;
                    case "2":
                        IncomingDayOffView();
                        break;
                    default:
                        PrintInputErrorMessage();
                        break;
                }
            }
        }
        /// <summary>
        /// İzinlerine 15 günden az kalan personelleri listeler
        /// </summary>
        private static void IncomingDayOffView()
        {
            PersonalManager personalManager = new PersonalManager();
            var personalList = personalManager.GetList().Where(pers => pers.DayOffStart < DateTime.Now.AddDays(+15) && pers.DayOffStart > DateTime.Now).ToList();

            foreach (var personal in personalList)
            {
                Console.WriteLine(personal.Name + " " + personal.Surname);
                Console.WriteLine(" İzin Başlangıç: " + personal.DayOffStart);
                Console.WriteLine(" İzin Bitiş: " + personal.DayOffEnd);
            }
        }

        private static void UpdateDayOffMenuView()
        {
            PersonalManager personalManager = new PersonalManager();
            var personalId = ChooseFromEntity<Personal>(personalManager, PrintDayOff, "İzni Güncellenecek Personel Numarası:");

            UpdateDayOff(personalManager.Find(personalId));
            Console.WriteLine("Güncelleme başarılı!");
        }
        /// <summary>
        /// Personellerin izin tarihlerini günceller
        /// </summary>
        /// <param name="personal"></param>
        private static void UpdateDayOff(Personal personal)
        {
            Console.WriteLine(personal.Id + ": " + personal.Name + " " + personal.Surname);

            Console.Write(" İzin başlangıç: ");
            var dayOffStart = Console.ReadLine().StringToDatetime();
            personal.DayOffStart = dayOffStart;

            Console.Write(" İzin bitiş: ");
            var dayOffEnd = Console.ReadLine().StringToDatetime();
            personal.DayOffEnd = dayOffEnd;

        }
        /// <summary>
        /// Personellerin izin tarihlerini listeler
        /// </summary>
        /// <param name="personal"></param>
        private static void PrintDayOff(Personal personal)
        {
            Console.WriteLine(personal.Id + ": " + personal.Name + " " + personal.Surname);
            Console.WriteLine(" İzin başlangıç: " + personal.DayOffStart);
            Console.WriteLine(" İzin bitiş: " + personal.DayOffEnd);
        }
        /// <summary>
        /// Departman ile alakalı operasyonları barındırır
        /// </summary>
        private static void DepartmentOperation()
        {
            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz işlemi giriniz: 1:Ekle / 2:Listele / 3:Güncelle / 4:Sil / 5:Departmana ait Personelleri Listele / 6 Anasayfa / 9:Çıkış");
                var operation = Console.ReadLine();

                switch (operation)
                {
                    case "6":
                        return;
                    case "9":
                        Environment.Exit(0);
                        break;
                    case "1":
                        AddDepartmentView();
                        break;
                    case "2":
                        GetDepartmentListView();
                        break;
                    case "3":
                        UpdateDepartmentMenuView();
                        break;
                    case "4":
                        DeleteDepartmentView();
                        break;
                    case "5":
                        PersonalListInDepartmentView();
                        break;
                    default:
                        PrintInputErrorMessage();
                        break;
                }
            }
        }
        /// <summary>
        /// Verilen departmanId'deki personelleri listeler.
        /// </summary>
        private static void PersonalListInDepartmentView()
        {
            DepartmentManager departmentManager = new DepartmentManager();
            GetDepartmentListView();
            Console.WriteLine("Departman seçin: ");
            int departmentId = Convert.ToInt32(Console.ReadLine());
            var personalList = departmentManager.PersonalListWithDepartmentId(departmentId);

            foreach (var personal in personalList)
            {
                Console.WriteLine(personal.Name + " " + personal.Surname);
            }

        }
        /// <summary>
        /// Departman silme fonksiyonu. Eğer departmanda personel varsa silinemez
        /// </summary>
        private static void DeleteDepartmentView()
        {
            DepartmentManager departmentManager = new DepartmentManager();
            PersonalManager personalManager = new PersonalManager();

            int id = ChooseFromEntity<Department>(departmentManager, PrintDepartment, "Silinecek Departman Numarası:");
            if (personalManager.AnyPersonalInDepartment(id))
            {
                Console.WriteLine("Silmeye çalıştığınız departmanda personel bulunmaktadır!");
                return;
            }
            Console.WriteLine("Departman silinecektir, onaylıyor musunuz? Evet(E), Hayır(H)");
            string approval = Console.ReadLine();
            if (approval.ToUpper() == "E")
            {
                if (departmentManager.Delete(id))
                    Console.WriteLine("Departman silindi!");
            }

        }

        /// <summary>
        /// Departman güncelleme aksiyonu
        /// </summary>
        private static void UpdateDepartmentMenuView()
        {
            var departmentManager = new DepartmentManager();
            var departmentId = ChooseFromEntity<Department>(departmentManager, PrintDepartment, "Güncellenecek Departman Numarası:");
            UpdateDepartment(departmentManager.Find(departmentId));
        }
        /// <summary>
        /// Parametre olarak gönderilen departmanın propertylerini yazdırma fonksiyonu
        /// </summary>
        /// <param name="department"></param>
        private static void PrintDepartment(Department department)
        {
            Console.WriteLine(department.Id + ": " + department.Name);
        }
        /// <summary>
        /// Departman güncelleme fonksiyonu
        /// </summary>
        /// <param name="department"></param>
        private static void UpdateDepartment(Department department)
        {
            string departmentName;
            while (true)
            {
                Console.Write("Departman Adı: ");
                departmentName = Console.ReadLine();

                if (departmentName != string.Empty)
                {
                    department.Name = departmentName;
                    break;
                }
                Console.WriteLine("Lütfen geçerli bir isim giriniz!");

            }
            
            Console.WriteLine("Güncelleme başarılı!");
        }
        /// <summary>
        /// Departmanların listesini getirme
        /// </summary>
        private static void GetDepartmentListView()
        {
            var departmentManager = new DepartmentManager();
            PrintDepartmentList(departmentManager);
        }
        /// <summary>
        /// Departman Ekleme
        /// </summary>
        private static void AddDepartmentView()
        {
            var newDepartment = GetDepartmentFromUser();

            var departmentManager = new DepartmentManager();

            departmentManager.Add(newDepartment);
            Console.WriteLine("Ekleme başarılı!");
        }
        /// <summary>
        /// Departman propertylerini getirme
        /// </summary>
        /// <returns></returns>
        private static Department GetDepartmentFromUser()
        {
            string departmentName;
            var department=new Department();
            while (true)
            {
                Console.Write("Departman Adı: ");
                departmentName = Console.ReadLine();

                if (departmentName != string.Empty)
                {
                    department.Name = departmentName;
                    break;
                }
                Console.WriteLine("Lütfen geçerli bir isim giriniz!");

            }
            return department;

        }
        /// <summary>
        /// Personel Operasyonu
        /// </summary>
        private static void PersonOperation()
        {
            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz işlemi giriniz: 1:Ekle / 2:Listele / 3:Güncelle / 4:Sil / 5:Anasayfa / 9:Çıkış");
                var operation = Console.ReadLine();

                switch (operation)
                {
                    case "5":
                        return;
                    case "9":
                        Environment.Exit(0);
                        break;
                    case "1":
                        AddPersonalView();
                        break;
                    case "2":
                        GetPersonListView();
                        break;
                    case "3":
                        UpdatePersonView();
                        break;
                    case "4":
                        DeletePersonView();
                        break;
                    default:
                        PrintInputErrorMessage();
                        break;
                }
            }


        }
        /// <summary>
        /// Personel silme fonksiyonu
        /// </summary>
        private static void DeletePersonView()
        {
            PersonalManager personalManager = new PersonalManager();
            int id = ChooseFromEntity<Personal>(personalManager, PrintPersonal, "Silienecek Personel Id'sini seçiniz:");

            Console.WriteLine("Personel silinecektir, onaylıyor musunuz? Evet(E), Hayır(H)");
            var approval = Console.ReadLine();

            if (approval.ToUpper() == "E")
            {
                if (personalManager.Delete(id))
                    Console.WriteLine("Personel silindi.");
            }
            else
                return;
        }
        
        /// <summary>
        /// Parametre olarak gönderilen personelin bilgilerini ekrana yazdırır.
        /// </summary>
        /// <param name="personal"></param>
        private static void PrintPersonal(Personal personal)
        {
            var department = new DepartmentManager().GetList().Where(department => department.Id == personal.DepartmentId).FirstOrDefault();
            Console.Write("Id: " + personal.Id + " => ");
            Console.WriteLine(personal.Name + " " + personal.Surname);
            Console.WriteLine("  Departman: " + department.Name);
            Console.WriteLine("  İzin Başlangıcı: " + personal.DayOffStart);
            Console.WriteLine("  İzin Sonu: " + personal.DayOffEnd);
        }
        /// <summary>
        /// Personel güncelleme aksiyonu seçildiğinde bu fonksiyon çalışır
        /// </summary>
        private static void UpdatePersonView()
        {
            var personalManager = new PersonalManager();
            var personalId = ChooseFromEntity<Personal>(personalManager, PrintPersonal, "Güncellenecek Personel Id'si:");
            UpdatePersonal(personalManager.Find(personalId));
        }
        /// <summary>
        /// Personel güncelleme fonksiyonu
        /// </summary>
        /// <param name="personal"></param>
        private static void UpdatePersonal(Personal personal)
        {
            Console.WriteLine("Güncellemek istediğiniz alanı giriniz(1:İsim, 2:Soyisim, 3:Departman) : ");
            int userInput;

            userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
            {
                case 1:
                    while (true)
                    {
                        Console.Write("İsim: ");
                        string name = Console.ReadLine();
                        if (name != string.Empty)
                        {
                            personal.Name = name;
                            break;
                        }
                        Console.WriteLine("Lütfen geçerli bir isim giriniz!");

                    }

                    break;
                case 2:
                    while (true)
                    {
                        Console.Write("Soyisim: ");
                        string surName = Console.ReadLine();
                        if (surName != string.Empty)
                        {
                            personal.Surname = surName;
                            break;
                        }
                        Console.WriteLine("Lütfen geçerli bir soyisim giriniz!");
                    }
                    break;
                case 3:
                    Console.Write("Departman: ");
                    int departmentId = ChooseDepartment();
                    personal.DepartmentId = departmentId;
                    break;
                default:
                    PrintInputErrorMessage();
                    break;
            }

            Console.WriteLine("Güncelleme başarılı!");
        }
        /// <summary>
        /// Personelleri listeleyip içindeki değerleri ekrana yazdırır
        /// </summary>
        private static void GetPersonListView()
        {
            var personalManager = new PersonalManager();
            var departmentManager = new DepartmentManager();
            foreach (var personal in personalManager.GetList())
            {
                PrintPersonal(personal);
            }
        }
        /// <summary>
        /// Personel ekleme fonksiyonu
        /// </summary>
        private static void AddPersonalView()
        {
            var newPersonal = GetPersonalFromUser();

            var personManager = new PersonalManager();
            personManager.Add(newPersonal);
            Console.WriteLine("Ekleme başarılı!");
        }
        /// <summary>
        /// Yeni eklenecek olan personelin bilgileri doldurulur.
        /// </summary>
        /// <returns></returns>
        private static Personal GetPersonalFromUser()
        {
            string name;
            while (true)
            {
                Console.WriteLine("İsim: ");
                name = Console.ReadLine();
                if (name != string.Empty)
                {
                    break;
                }
                Console.WriteLine("Lütfen geçerli bir isim giriniz!");

            }
            string surname;
            while (true)
            {
                Console.WriteLine("Soyisim: ");
                surname = Console.ReadLine();
                if (surname != string.Empty)
                {
                    break;
                }
                Console.WriteLine("Lütfen geçerli bir soyisim giriniz!");
            }

            var departmentId = ChooseDepartment();

            var personal = new Personal
            {
                Name = name,
                Surname = surname,
                DepartmentId = departmentId,
            };
            return personal;
        }
        /// <summary>
        /// Kullanıcıdan string olarak alınan inputları DateTime'ye çevirir
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static DateTime EnterDatetime(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                var userInput = Console.ReadLine().StringToDatetime();

                if (userInput != null)
                    return (DateTime)userInput;

                Console.WriteLine("Lütfen geçerli bir tarih giriniz.");
            }
        }
        /// <summary>
        /// EntityRepositoryBase listesini yazdırıp içindeki entityler kullanıcıya sunulur ve entitylerden birinin Id'sini seçmesi söylenir, kullanıcı seçer ve seçtiği Id alınır.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityRepo"></param>
        /// <param name="entityPrinter"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static int ChooseFromEntity<T>(EntityRepositoryBase<T> entityRepo, Action<T> entityPrinter, string message) where T : class, IEntity, new()
        {
            while (true)
            {
                foreach (var entity in entityRepo.GetList())
                {
                    entityPrinter(entity);
                }
                Console.Write(message);
                int userInput;

                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    PrintInputErrorMessage();
                    continue;
                }

                if (!entityRepo.Any(userInput))
                {
                    PrintInputErrorMessage();
                    continue;
                }
                return userInput;
            }
        }
        /// <summary>
        /// Departmanları listeler ve kullanıcıdan 1 tanesinin Idsini seçmesi istenir, kullanıcının seçtiği Id alınır.
        /// </summary>
        /// <returns></returns>
        private static int ChooseDepartment()
        {
            DepartmentManager departmentManager = new DepartmentManager();
            return ChooseFromEntity<Department>(departmentManager, PrintDepartment, "Departman Seçiniz: ");

        }
        /// <summary>
        /// Geçersiz değerler girildiğinde uyarı verir
        /// </summary>
        private static void PrintInputErrorMessage()
        {
            Console.WriteLine("Lütfen geçerli bir seçenek giriniz!");
        }
        /// <summary>
        /// Departman listesindeki departmanların propertylerini yazdırma
        /// </summary>
        /// <param name="departmentManager"></param>
        private static void PrintDepartmentList(DepartmentManager departmentManager)
        {
            Console.WriteLine("Departman Listesi: ");
            foreach (var department in departmentManager.GetList())
            {
                Console.WriteLine(department.Id + ": " + department.Name);
            }
        }
        /// <summary>
        /// Giriş bilgileri kontrolü
        /// </summary>
        public static void Login()
        {
            while (true)
            {
                Console.Write("Kullanıcı Adı: ");
                string userName = Console.ReadLine();
                Console.Write("Şifre: ");
                string password = Console.ReadLine();

                if (userName == "sa" && password == "123")
                    break;

                Console.WriteLine("Kullanıcı Adı veya Şifre yanlış !");
            }
        }
    }
}
