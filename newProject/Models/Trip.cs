using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace newProject.Models 
{  
    public class Trip
    {
        public int TripId {get; set; }
        public string Destination {get; set; } // Trip name

        public string Description {get; set; }

        public DateTime TravelStartDate {get; set; }

        public DateTime TravelEndDate {get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get;set;}

        // And a traveler
        public int UserId {get; set; } 
        // because we have a userId in Trip we need
        // to have the user present in that table       
       
        public User User {get; set; }  
        
        // List of travels       
        public List<Plan> Plans {get;set;}

        public Trip() {
            Plans = new List<Plan>();
        } 
    }
}