using System.Dynamic;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using OOD_first_project;
using System.Xml.Linq;

[JsonDerivedType(typeof(Crew))]
[JsonDerivedType(typeof(AirPort))]
[JsonDerivedType(typeof(Passenger))]
[JsonDerivedType(typeof(PassengerPlane))]
[JsonDerivedType(typeof(Cargo))]
[JsonDerivedType(typeof(CargoPlane))]
[JsonDerivedType(typeof(Flight))]

public abstract class Data
{
    public UInt64 ID { get; set; }
    public Data(UInt64 ID)
    {
        this.ID = ID;
    }
}

public class Crew:Data
{
    
    public string Name { get; set; }
    public UInt64 Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public UInt16 practice { get; set; }
    public string Role { get; set; }
    public Crew(ulong iD,string name,ulong age, string phone, string email, ushort practice, string role):base(iD)
    {
        
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
        this.practice = practice;
        Role = role;
    }
}

public class Passenger:Data
{

    public string name { get; set; }
    public UInt64 Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Class { get; set; }
    public UInt64 Miles { get; set; }
    public Passenger(ulong iD,string name,ulong age,  string phone, string email, string @class, ulong miles):base(iD)
    {
       
        this.name = name;
        Age = age;
        Phone = phone;
        Email = email;
        Class = @class;
        Miles = miles;
    }
}

public class CargoPlane:Data,IReportable
{
   
    public string Serial { get; set; }
    public string Country { get; set; }
    public string Model { get; set; }
    public Single MaxLoad { get; set; }
    public CargoPlane(ulong iD, string serial, string country, string model, float maxLoad):base(iD) 
    {
        
        Serial = serial;
        Country = country;
        Model = model;
        MaxLoad = maxLoad;
    }
    public string Name => Model;
    public string ReportIdentifier => $"Serial: {Serial}, Type: {Name}";
    public string Accept(INewsVisitor visitor)
    {
        return visitor.Visit(this);
    }
}

public class Cargo:Data
{
    
    public Single Weight { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public Cargo(ulong iD, float weight, string country, string description):base(iD)
    {
        
        Weight = weight;
        Country = country;
        Description = description;
    }
}

public class PassengerPlane:Data, IReportable
{
    
    public string Serial { get; set; }
    public string Country { get; set; }
    public string Model { get; set; }
    
    public UInt16 FirstClassSize { get; set; }
    public UInt16 BusinessClassSize { get; set; }
    public UInt16 EconomyClassSize { get;set; }
    public PassengerPlane(ulong iD,string serial, string country, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize):base(iD) 
    {
        
        Serial = serial;
        Country = country;
        Model = model;
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
    }
    public string Name => Model;
    public string ReportIdentifier => $"Serial: {Serial}, Type: {Name}";
    public string Accept(INewsVisitor visitor)
    {
        return visitor.Visit(this);
    }
}

public class AirPort:Data,IReportable
{
    
    public string Name { get; set; }   
    public string Code { get; set; }
    public Single Longitude { get; set; }
    public Single Latitude { get; set; }
    public Single AMSL { get; set; }
    public string Country { get; set; }
    public AirPort(ulong iD,string name, string code,float longitude, float latitude, float aMSL, string country):base(iD) 
    {
        
        Name = name;
        Code = code;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = aMSL;
        Country = country;
    }
    public string ReportIdentifier => Name;
    public string Accept(INewsVisitor visitor)
    {
        return visitor.Visit(this);
    }
}

public class Flight:Data
{
   
    public UInt64 OriginID { get; set; }
    public UInt64 TargetID { get; set;}
    public string TakeoffTime { get; set; }
    public string LandingTime { get; set; }
    public Single Longitude { get; set; }
    public Single Latitute { get; set; }
    public Single AMSL { get;set; }
    public UInt64 PlaneID { get;set; }
    public List<ulong> CrewIDs { get; set; }
    public List<ulong> LoadIDs { get; set; }
    public Flight(ulong iD, ulong originID, ulong targetID,string landingTime, string takeoffTime,float longitude, float latitute,float amsl,ulong planeID,   List<ulong> CrewiDs,List<ulong> loadids):base(iD) 
    {
        
        OriginID = originID;
        TargetID = targetID;
        LandingTime = landingTime;
        TakeoffTime = takeoffTime;
        Longitude = longitude;
        Latitute = latitute;
        AMSL = amsl;
        PlaneID = planeID;
        CrewIDs = CrewiDs;
        LoadIDs = loadids;
    }
}
public class IDUpdateArgs : EventArgs
{
    public UInt64 ObjectID { get; init; }
    public UInt64 NewObjectID { get; init; }
}

public class PositionUpdateArgs : EventArgs
{
    public UInt64 ObjectID { get; init; }
    public Single Longitude { get; init; }
    public Single Latitude { get; init; }
    public Single AMSL { get; init; }
}

public class ContactInfoUpdateArgs : EventArgs
{
    public UInt64 ObjectID { get; init; }
    public string PhoneNumber { get; init; }
    public string EmailAddress { get; init; }
}