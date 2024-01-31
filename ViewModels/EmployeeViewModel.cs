using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
using MvvmDemo.Commands;
using MvvmDemo.Models;
using System.Collections.ObjectModel;
namespace MvvmDemo.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        EmployeeService ObjEmployeeService;
        #region Constructorpart
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #endregion

        #region DisplayListOperation
        private ObservableCollection<Employee> employeesList;
        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value;OnPropertyChanged("EmployeesList"); }
        }
        private void LoadData()
        {
            EmployeesList =new ObservableCollection<Employee>( ObjEmployeeService.GetAll());
        }
        #endregion

        #region DataMemberDeclaration For CRUD
        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value;OnPropertyChanged("CurrentEmployee"); }
        }
        private string messege;
        public string Messege
        {
            get { return messege; }
            set { messege = value; OnPropertyChanged("Messege"); }
        }
        #endregion

        #region SaveOperation
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
          

        }
        public void Save()
        {
            try
            {
                var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                    Messege = "Employee Saved";
                else
                    Messege = "Save option failed";
            }
            catch(Exception ex)
            {
                Messege = ex.Message;
            }
        }
        #endregion

        #region SearchOperation
        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
           
        }

        public void Search()
        {
            try
            {
                var objEmployee = ObjEmployeeService.Search(CurrentEmployee.Id);
                if(objEmployee!=null)
                {
                    CurrentEmployee.Name = objEmployee.Name;
                    CurrentEmployee.Age = objEmployee.Age;
                }
                else
                {
                    Messege = "Employee not found";
                }
            }
            catch(Exception ex)
            {
                Messege = ex.Message;
            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
            
        }
        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeeService.Update(CurrentEmployee);
                if(IsUpdated)
                {
                    Messege = "Employee updated";
                    LoadData();
                }
                else
                {
                    Messege = "Employee update operation failed";
                }
            }
            catch(Exception ex)
            {
                Messege = ex.Message;
            }
        }
        #endregion

        #region DeleteOperation

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
          
        }
        public void Delete()
        {
            try
            {
                var IsDeleted = ObjEmployeeService.Delete(CurrentEmployee.Id);
               if(IsDeleted)
                {
                    Messege = "Employee deleted";
                    LoadData();
                }
                else
                {
                    Messege = "delete operation failed";
                }


            }
            catch(Exception Ex)
            {
                Messege = Ex.Message;
            }
        }
        #endregion

    }
}
