using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Incident
{
    public Guid IncidentId { get; set; }

    public string? Description { get; set; }

    public string? Incident1 { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public Guid? ModifiedBy { get; set; }
}
