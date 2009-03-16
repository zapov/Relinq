// This file is part of the re-motion Core Framework (www.re-motion.org)
// Copyright (C) 2005-2008 rubicon informationstechnologie gmbh, www.rubicon.eu
// 
// The re-motion Core Framework is free software; you can redistribute it 
// and/or modify it under the terms of the GNU Lesser General Public License 
// version 3.0 as published by the Free Software Foundation.
// 
// re-motion is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with re-motion; if not, see http://www.gnu.org/licenses.
// 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Remotion.Utilities;

namespace Remotion.Data.Linq.Clauses
{
  /// <summary>
  /// Represents the whole order by part of a linq query.
  /// example: orderby expression
  /// </summary>
  public class OrderByClause : IBodyClause
  {
    private readonly List<OrderingClause> _orderingList = new List<OrderingClause>();
    
    /// <summary>
    /// Initialize a new instance of <see cref="OrderByClause"/>
    /// </summary>
    /// <param name="ordering"><see cref="OrderingClause"/></param>
    public OrderByClause (OrderingClause ordering)
    {
      ArgumentUtility.CheckNotNull ("ordering", ordering);
      _orderingList.Add (ordering);
    }

    public QueryModel QueryModel { get; private set; }

    /// <summary>
    /// A collection of <see cref="OrderingClause"/>
    /// </summary>
    public ReadOnlyCollection<OrderingClause> OrderingList
    {
      get { return new ReadOnlyCollection<OrderingClause>(_orderingList); }
    }

    public void Add(OrderingClause ordering)
    {
      ArgumentUtility.CheckNotNull ("ordering", ordering);
      _orderingList.Add (ordering);
    }

    public IClause PreviousClause
    {
      get { return _orderingList[0].PreviousClause; }
    }

    public virtual void Accept (IQueryVisitor visitor)
    {
      ArgumentUtility.CheckNotNull ("visitor", visitor);
      visitor.VisitOrderByClause (this);
    }
    
    public void SetQueryModel (QueryModel model)
    {
      ArgumentUtility.CheckNotNull ("model", model);
      if (QueryModel != null)
        throw new InvalidOperationException("QueryModel is already set");
      
      QueryModel = model;
    }

    public OrderByClause Clone (IClause newPreviousClause)
    {
      var firstOrderingClone = OrderingList[0].Clone (newPreviousClause);
      var clone = new OrderByClause (firstOrderingClone);

      for (int i = 1; i < OrderingList.Count; ++i)
      {
        var orderingClone = OrderingList[i].Clone (clone.OrderingList[i - 1]);
        clone.Add (orderingClone);
      }
      return clone;
    }

    IBodyClause IBodyClause.Clone (IClause newPreviousClause)
    {
      return Clone (newPreviousClause);
    }
  }
}