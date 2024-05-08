using BloodBank.Models;

namespace BloodBank.UnitOfWork
{
    public interface ICRUD <T> where T : class
    {
        public T GetAllList(T data, int i);
        public int GetAll(T data);
        public void CreateHospital(Hospital hospital, Guid IDN);
        public void Createdonor(Donor data,Guid ID);
        public void Createblood(Blood blood, Guid IDN, Guid IDD);
        public T GetByName(string Name);
        public string UpdateByID(T w, Guid id);

        public string DeleteByID(Guid id);


    }
}
