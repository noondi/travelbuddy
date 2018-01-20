using System;
using System.Collections.Generic;

namespace newProject.Models 
{  
    public class User: BaseEntity
    {
        public int UserId {get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }        
        public string Email {get; set; }         
          
        public DateTime BirthDate {get; set; } 

        public string Password {get; set; }   
        
        // // A list of trips
        // public List<Trip> Trips {get; set; }       
       
        // A list of plans
        public List<Plan> Plans {get;set;}
        public User() 
        {
            // Trips = new List<Trip>();
            Plans = new List<Plan>();
        }  
    }
}