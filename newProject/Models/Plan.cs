using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using newProject.Models;

namespace newProject.Models
{
    public class Plan : BaseEntity
    {
       
        public int PlanId {get;set;}  
       

        public int TripId {get;set;}
        public Trip Trip {get;set;}
        
        public int UserId {get;set;}
        public User User {get;set;}

    }
}