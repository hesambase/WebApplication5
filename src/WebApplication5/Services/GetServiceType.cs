using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication2.Models;

namespace WebApplication4.Services
{

    public class GetServiceType
    {
        private string _serviceName;
        public GetServiceType(string ServiceName)
        {
            _serviceName = ServiceName;

        }
        public string intServiceType(string ServiceName)
        {
           string SRV;
            _serviceName = ServiceName;
            switch (_serviceName)
            {
                case "Nezafat":
                    SRV = "0";
                    break;
                case "Mehmandari":
                    SRV = "1";
                    break;
                case "Ghalishooyee":
                    SRV = "2";
                    break;
                case "Naghashi":
                    SRV = "3";
                    break;
                default:
                    SRV= "0";
                    break;
            }
            return SRV;

        }
    }
}
