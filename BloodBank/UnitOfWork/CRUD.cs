using BloodBank.Controllers;
using BloodBank.Models;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Database = Microsoft.EntityFrameworkCore.Storage.Database;

namespace BloodBank.UnitOfWork
{
    public class CRUD<T> : ICRUD <T> where T : class
    {

       public Data databse;

        public CRUD(Data database)
        {

            this.databse = database;
        }
        public T GetAllList(T data,int i)
        {
   


            T t = databse.Set<T>().FromSqlRaw($"SELECT TOP 1 * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum FROM dbo.{typeof(T).Name}s) AS SubQuery WHERE RowNum > {i}").FirstOrDefault();
      
            
            return t;
        }
        public int GetAll(T data)
        {
            string tableName = typeof(T).Name + "s";
           
            List<T> list = databse.Set<T>().FromSqlRaw($"SELECT * FROM dbo.{tableName}").ToList();
            return list.Count;
        }
        public T GetByName(string Name)
        {

            T? ww = databse.Set<T>().FromSqlRaw($"SELECT * FROM dbo.{typeof(T).Name}s WHERE Name = @Name", new SqlParameter("@Name", Name)).FirstOrDefault();
            return ww;
        }
        public void Createdonor(Donor data,Guid ID)
        {
            //   databse($"INSERT INTO dbo.Donors (Id, Name, Address, Gender, Diabetes, BloodPressure, PhoneNumber, Age, Nurses, Email) VALUES ({data.Id},{data.Name},{data.Address},{data.Gender},{data.Diabetes},{data.BloodPressure},{data.PhoneNumber},{data.Age},{data.Nurses},{data.Email})");
            string insertStatement = $"INSERT INTO dbo.Donors (Id, Name, Address, Gender, Diabetes, BloodPressure, PhoneNumber, Age, NursesId, Email) " +
                                  $"VALUES (@Id, @Name, @Address, @Gender, @Diabetes, @BloodPressure, @PhoneNumber, @Age, @NurseId, @Email)";

            databse.Database.ExecuteSqlRaw(insertStatement,
                new SqlParameter("@Id", data.Id),
                new SqlParameter("@Name", data.Name),
                new SqlParameter("@Address", data.Address),
                new SqlParameter("@Gender", data.Gender),
                new SqlParameter("@Diabetes", data.Diabetes),
                new SqlParameter("@BloodPressure", data.BloodPressure),
                new SqlParameter("@PhoneNumber", data.PhoneNumber),
                new SqlParameter("@Age", data.Age),
                new SqlParameter("@NurseId", ID),
                new SqlParameter("@Email", data.Email)
            );   
            databse.SaveChanges();
        }
        public void Createblood(Blood blood, Guid IDN,Guid IDD)
        {
            //   databse($"INSERT INTO dbo.Donors (Id, Name, Address, Gender, Diabetes, BloodPressure, PhoneNumber, Age, Nurses, Email) VALUES ({data.Id},{data.Name},{data.Address},{data.Gender},{data.Diabetes},{data.BloodPressure},{data.PhoneNumber},{data.Age},{data.Nurses},{data.Email})");
            string insertStatement = $"INSERT INTO dbo.Bloods (Id,BloodType, DonorsId, NursesId,Availability) " +
                                  $"VALUES (@Id,@BloodType, @DonorsId, @NursesId,@Availability)";

            Guid wwww = Guid.NewGuid();
            databse.Database.ExecuteSqlRaw(insertStatement,
                  new SqlParameter("@Id", wwww),
                new SqlParameter("@BloodType", blood.BloodType),
                new SqlParameter("@DonorsId", IDD),
                new SqlParameter("@NursesId", IDN),
                new SqlParameter("@Availability","1")


            );
            databse.SaveChanges();
        }
        public void CreateHospital(Hospital hospital, Guid IDN)
        {
            //   databse($"INSERT INTO dbo.Donors (Id, Name, Address, Gender, Diabetes, BloodPressure, PhoneNumber, Age, Nurses, Email) VALUES ({data.Id},{data.Name},{data.Address},{data.Gender},{data.Diabetes},{data.BloodPressure},{data.PhoneNumber},{data.Age},{data.Nurses},{data.Email})");
            string insertStatement = $"INSERT INTO dbo.hospitals (Name,address, Email, PhoneNumber,BloodId) " +
                                  $"VALUES (@Name,@address, @Email, @PhoneNumber,@BloodId)";

            databse.Database.ExecuteSqlRaw(insertStatement,
                  new SqlParameter("@Name", hospital.Name),
                new SqlParameter("@address", hospital.address),
                new SqlParameter("@Email",hospital.Email),
                 new SqlParameter("@PhoneNumber", hospital.PhoneNumber),
                new SqlParameter("@BloodId",IDN)


            );

            var bloodRecord = databse.Bloods.FromSqlRaw($"SELECT * FROM dbo.bloods WHERE Id = '{IDN}'").FirstOrDefault();

            if (bloodRecord != null)
            {
                // Update the availability of the blood record
                databse.Database.ExecuteSqlRaw($"UPDATE dbo.bloods SET Availability = '0' WHERE Id = '{IDN}'");
            }
            databse.SaveChanges();
        }

        public string DeleteByID(Guid id)
        {

            string insertStatement = $"DELETE FROM dbo.{typeof(T).Name}s WHERE  Id = @id";

            databse.Database.ExecuteSqlRaw(insertStatement,
                  new SqlParameter("@id", id)
             


            );

        
            databse.SaveChanges();
            return ($"Sucssefuly removed");

        }

        public string UpdateByID(T w, Guid id)
        {
            IQueryable<T> ww = databse.Set<T>().FromSqlRaw($"SELECT * FROM dbo.{typeof(T).Name}s  WHERE ID = {id}");
            if (ww == null)
            {
               
                return ("not found");
            }
            ww= (IQueryable<T>)w;
            databse.SaveChanges();
            return ($"Sucssefuly Updated");

        }
    }
}
