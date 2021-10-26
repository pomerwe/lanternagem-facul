using lanternagem_api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lanternagem_api.Models
{
  public class InsuranceCompany : IEntity
  {
    [Key]
    public int Id { get; set; }
    [JsonIgnore]
    public List<InsuranceBranch> Children { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }

    public InsuranceCompany()
    {
      Children = new List<InsuranceBranch>();
    }

    public void AddChildBranch(InsuranceBranch insuranceBranch)
    {
      if (insuranceBranch == null)
        throw new Exception("InsuranceBranch is not set!");

      if (Children.Contains(insuranceBranch))
        throw new Exception("This branch is already linked to this company!");

      Children.Add(insuranceBranch);
    }

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
}