using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Testing
{
    public int Id { get; set; }

    public int Kerdoivid { get; set; }

    public string? Title { get; set; }

    public string? Taskdesc { get; set; }

    public string? Testingtype { get; set; }

    public string? Appurl { get; set; }

    public string? Os { get; set; }

    public string? Browser { get; set; }

    public string? Resolution { get; set; }

    public string? Additionalreq { get; set; }

    public string? Stepstoreproduce { get; set; }

    public string? Expectedresult { get; set; }

    public string? Actualresult { get; set; }

    public string? Defaultseverity { get; set; }

    public string? Defaultpriority { get; set; }

    public bool? Requireattachments { get; set; }

    public virtual ICollection<KTobbi> KTobbis { get; set; } = new List<KTobbi>();

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;

    public virtual ICollection<Testingcase> Testingcases { get; set; } = new List<Testingcase>();

    public virtual ICollection<Testingevaluation> Testingevaluations { get; set; } = new List<Testingevaluation>();

    public virtual ICollection<Testingscenario> Testingscenarios { get; set; } = new List<Testingscenario>();

    public virtual ICollection<Testingtool> Testingtools { get; set; } = new List<Testingtool>();
}
