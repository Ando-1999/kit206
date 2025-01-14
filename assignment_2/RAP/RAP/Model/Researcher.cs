﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    public abstract class Researcher
    {
        private int? id;
        public int? Id
        {
            get { return id; }
            set { id = value; }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private Uri photo;
        public Uri Photo
        {
            get { return photo; }
            set { photo = value; }
        }
        private DateTime? startInstitution;
        public DateTime? StartInstitution
        {
            get { return startInstitution; }
            set { startInstitution = value; }
        }
        private List<Model.Position> positions;
        public List<Model.Position> Positions
        {
            get { return positions; }
            set { positions = value; }
        }

        private Campus campus;
        public Campus Campus { get; set; }

        private string unit;
        public string Unit { get; set; }

        public Researcher()
        {
            Positions = new List<Model.Position>();
        }

        // Total number of publications authored.
        public int? numberOfPublications()
        {
            return Database.PublicationAdapter.totalPublications(this);
        }

        // Get all of researcher's publications.
        public List<Publication> getPublications()
        {
            return Database.PublicationAdapter.fetchPublicationsList(this);
        }

        // List of all positions ever occupied at institution.
        public List<Position> getPositions()
        {
            return Database.ResearcherAdapter.fetchPositions(this);
        }

        public override string ToString()
        {
            return $"{LastName}, {FirstName} ({Title})";
        }
        public abstract string ToFullString();
    }
}
