using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using WorkerLogger.Domain.Entities.Authentication;
using WorkerLogger.Domain.Entities.WorkInformations;
using WorkerLogger.Domain.WorkInformations;
using WorkerLogger.WPFClient.Models;

namespace WorkerLogger.WPFClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {


        public ObservableCollection<WorkInformationDTO> WorkInformations
        {
            get => workInformations;
            set
            {
                workInformations = value;
                OnPropertyChanged();
            }
        }


        public WorkInformationDTO SelectedWorkInformation
        { 
            get { return selectedWorkInformation; }
            set { 
                selectedWorkInformation = value;
                OnPropertyChanged();
                
            }
        
        }

        public ICommand CreateWorkInformationCommand { get; set; }

        public ICommand EditWorkInformationCommand { get; set; }

        public ICommand ReportThisMonthCommand { get; set; }


        private ObservableCollection<WorkInformationDTO> workInformations;

        private WorkInformationDTO selectedWorkInformation;



        private HttpClient httpClient;

        private TokenModel token;

        public MainWindowViewModel()
        {
            SelectedWorkInformation = new WorkInformationDTO(Guid.Empty, "", "", new TimeSpan(0,0,0), DateTime.Now);
            CreateWorkInformationCommand = new RelayCommand(async () =>
            { 
            //Elküldjük a szükséges adatokat az endpointnak az új elem létrehozásához
                string endPoint = "Log";
                var response = await httpClient.PostAsJsonAsync<CreateWorkInformationCommand>(endPoint,
                    new Models.CreateWorkInformationCommand(token.UserId,
                    SelectedWorkInformation.Title,
                    SelectedWorkInformation.Description,
                    SelectedWorkInformation.TimeSpent));
                GetWorkInformations();

            });
            EditWorkInformationCommand = new RelayCommand(async () =>
            {
                //Elküldjük a szükséges adatokat az endpointnak a meglévő elem módosításához
                string endPoint = "Log";
                if(selectedWorkInformation.Id != Guid.Empty) {
                    var response = await httpClient.PatchAsJsonAsync<UpdateWorkInformationCommand>(endPoint,
                        new UpdateWorkInformationCommand(SelectedWorkInformation.Id,
                        SelectedWorkInformation.Title,
                        SelectedWorkInformation.Description,
                        SelectedWorkInformation.TimeSpent));
                }
                GetWorkInformations();
            });

            ReportThisMonthCommand = new RelayCommand(() =>
            {
                var hours = new TimeSpan(WorkInformations
                    .Where(x => x.CreationTime.Month == DateTime.Now.Month && x.CreationTime.Year == DateTime.Now.Year)
                    .Sum(x => x.TimeSpent.Ticks));
                var result = new MonthReport(DateTime.Now.Year, DateTime.Now.Month, hours.Hours);

                File.WriteAllText(token.UserId, JsonConvert.SerializeObject(result));
            });
        }
       

        private async void GetWorkInformations()
        {
            //Lekérjük a felhasználó saját bejegyzéseit
            string endPoint = string.Format("Log/{0}", token.UserId);
            var response = await httpClient.GetAsync(endPoint,HttpCompletionOption.ResponseContentRead);

            var info = await response.Content.ReadFromJsonAsync<ObservableCollection<WorkInformationDTO>>();
            if(info == null)
            {
                info = new ObservableCollection<WorkInformationDTO>();
            }    
            WorkInformations = info;
            SelectedWorkInformation = new WorkInformationDTO(Guid.Empty, "", "", new TimeSpan(0, 0, 0), DateTime.Now);
        }

        

        private void ConfigureHttpClient()
        {
            //Konfiguráljuk a Http Klienst
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5097");
            httpClient.DefaultRequestHeaders.Accept.Add(
              new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);


        }


        public void SetToken(TokenModel model)
        { 
            //Átvesszük a token-t a bejelentkező ablaktól
            token = model;
            ConfigureHttpClient();
            GetWorkInformations();
            
        }
    }
}
