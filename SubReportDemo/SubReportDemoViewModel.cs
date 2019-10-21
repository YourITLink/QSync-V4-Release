using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace SubReportDemo
{
    public class SubReportDemoViewModel
    {
        private MainWindow _mainWindow;
        private ReportViewer _reportViewer;
        public SubReportDemoViewModel(MainWindow window)
        {
            _mainWindow = window;
            _reportViewer = window.reportViewer;
            Initialize();

        }

        private IEnumerable<Department> departments = new List<Department>()
           {
               new Department() {ID = 1, Name = "Applied Mathematics" },
               new Department() {ID = 2, Name = "Software" },
               new Department() {ID = 3, Name = "Machine Learning" },
               new Department() {ID = 4, Name = "Petroleum Engineering" },
           };
        private void Initialize()
        {
            _reportViewer.LocalReport.DataSources.Clear();
            var departmentsModels = new ReportDataSource() { Name = "Department_DS", Value = departments };
            _reportViewer.LocalReport.DataSources.Add(departmentsModels);
            var path = Path.GetDirectoryName(Path.GetDirectoryName
                        (Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var MainPage = path + @"\subreportDemo\MainReport.rdlc";
            _reportViewer.LocalReport.ReportPath = MainPage;
            _reportViewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            _reportViewer.LocalReport.EnableExternalImages = true;
         //   _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
         //   _reportViewer.Refresh();
         //   _reportViewer.RefreshReport();

        }
        private List<Employee> Employees = new List<Employee>()
                {
                new Employee() {ID = 1, Name = "Sam", Gender ="male", Email="sam@gmail.com"},
                new Employee() {ID = 1, Name = "Ella",Gender ="female", Email="Ella@gmail.com" },
                new Employee() {ID = 1, Name = "TG",Gender ="male", Email ="TG@gmail.com" },
                new Employee() {ID = 1, Name = "Favor",Gender ="female", Email="Favor@gmail.com" },
                new Employee() {ID = 2,Name = "Micheal",Gender ="male",Email ="Micheal@gmail.com" },
                new Employee() {ID = 2, Name = "Joe",Gender ="male", Email ="Joe@gmail.com" },
                new Employee() {ID = 2, Name="Maintain",Gender ="female",Email ="Maintain@gmail.com" },
                new Employee() {ID = 3, Name = "Akeem",Gender ="male", Email ="Akeem@gmail.com" },
                new Employee() {ID = 3, Name = "Boye",Gender ="male", Email ="Boye@gmail.com" },
                new Employee() {ID = 4, Name ="Chioma",Gender ="female",Email ="Chioma@gmail.com" },
                new Employee() {ID = 4, Name = "Ofure",Gender ="female", Email ="Ofure@gmail.com" },
                new Employee() {ID = 4, Name = "Hart",Gender ="male",Email ="Hart@gmail.com" }
};
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var ID = Convert.ToInt32(e.Parameters[0].Values[0]);
            var employeegroup = Employees.FindAll(x => x.ID == ID);
            if (e.ReportPath == "EmployeeDetails")
            {
                var employeeDetails = new ReportDataSource() { Name = "Employee_DS", Value = employeegroup };
                e.DataSources.Add(employeeDetails);
            }
        }
    }
}
