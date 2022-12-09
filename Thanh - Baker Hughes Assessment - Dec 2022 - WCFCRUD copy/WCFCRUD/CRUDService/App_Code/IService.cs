using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    [OperationContract]
    List<UserData> GetAllUser();

    [OperationContract]
    void Insert(int VIN, int maker, int year, string model, DateTime inspectionDate, string inspector_name, int inspection_result, string notes);

    [OperationContract]
    void Update(int VIN, int maker, int year, string model, DateTime inspectionDate, string inspector_name, int inspection_result, string notes);

    [OperationContract]
    void Delete(int VIN);
}

[DataContract]
public class UserData
{
    [DataMember]
    public int VIN { get; set; }

    [DataMember]
    public int vehicle_maker { get; set; }

    [DataMember]
    public int vehicle_year { get; set; }

    [DataMember]
    public string vehicle_model { get; set; }

    [DataMember]
    public DateTime inspection_date { get; set; }

    [DataMember]
    public string inspector_name { get; set; }

    [DataMember]
    public string inspection_location { get; set; }

    [DataMember]
    public int inspection_result { get; set; }

    [DataMember]
    public string notes { get; set; }
}












