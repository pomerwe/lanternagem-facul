using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Domain
{
    public class Manager : User, IEntity
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public List<InsuranceBranch> ManagedBranches { get; set; }
        public SystemUser User { get; set; }

        public Manager()
        {
            ManagedBranches = new List<InsuranceBranch>();
        }

        public void AddNewManagedBranch(InsuranceBranch insuranceBranch)
        {
            if (insuranceBranch == null)
                throw new Exception("Insurance branch is not set!");

            if (ManagedBranches.Contains(insuranceBranch))
                throw new Exception("This branch is already managed by this manager!");

            ManagedBranches.Add(insuranceBranch);
        }

        public override string GetName()
        {
            return Name;
        }

        public override string GetCPF()
        {
            return CPF;
        }
    }
}
