﻿using System.Collections.Generic;
using System.Diagnostics;
using PortfolioSmarts.Domain.Contract.Interfaces;

namespace PortfolioSmarts.Domain.Contract.Portfolio
{
	[DebuggerDisplay("Security={Security.Symbol} Shares={Shares}")]
	public class Position : IDomainEntity
	{
		public long Id { get; set; }
		public Account Account { get; set; }
		public Security Security { get; set; }
		public decimal Shares { get; set; }
		public IDictionary<string, object> ExtraData { get; set; }
	}
}
