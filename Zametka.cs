using System;


namespace EjednevnicDZ
{
    public class Zametka
    {
        private DateTime? date;

        public string title { get; set; }
        public string description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
            }
        }
        
    
        
    }
}
