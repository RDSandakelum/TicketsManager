using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface IBudgetAllocationRepository
{
    DbSet<BudgetAllocationEntity> BudgetAllocations { get; set; }
}
