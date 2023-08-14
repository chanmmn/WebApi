using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string? FullName { get; set; }

    public string? Icno { get; set; }

    public string? Addressline1 { get; set; }

    public string? Addressline2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Postcode { get; set; }

    public string? Country { get; set; }

    public string? MobileNumber { get; set; }

    public string? Email { get; set; }

    public Guid? ReferredBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public string? Status { get; set; }
}
