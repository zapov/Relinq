// Copyright (c) rubicon IT GmbH, www.rubicon.eu
//
// See the NOTICE file distributed with this work for additional information
// regarding copyright ownership.  rubicon licenses this file to you under 
// the Apache License, Version 2.0 (the "License"); you may not use this 
// file except in compliance with the License.  You may obtain a copy of the 
// License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the 
// License for the specific language governing permissions and limitations
// under the License.
// 
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

namespace Remotion.Linq.Parsing.Structure.IntermediateModel
{
	/// <summary>
	/// Represents a <see cref="MethodCallExpression"/> for <see cref="Queryable.Max{TSource}"/> or <see cref="Queryable.Max{TSource,TResult}"/>.
	/// It is generated by <see cref="ExpressionTreeParser"/> when an <see cref="Expression"/> tree is parsed.
	/// When this node is used, it marks the beginning (i.e. the last node) of an <see cref="IExpressionNode"/> chain that represents a query.
	/// </summary>
	public class MaxExpressionNode : ResultOperatorExpressionNodeBase
	{
		public static readonly MethodInfo[] SupportedMethods =
			new[]
			{
				GetSupportedMethod (() => Queryable.Max<object> (null)),
				GetSupportedMethod (() => Queryable.Max<object, object> (null, null)),
				GetSupportedMethod (() => Enumerable.Max<object> (null)),
				GetSupportedMethod (() => Enumerable.Max<object, object> (null, null)),

				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<decimal>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<decimal?>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<double>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<double?>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<int>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<int?>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<long>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<long?>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<float>) null)),
				GetSupportedMethod (() => Enumerable.Max ((IEnumerable<float?>) null)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (decimal) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (decimal?) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (double) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (double?) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (int) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (int?) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (long) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (long?) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (float) 0)),
				GetSupportedMethod (() => Enumerable.Max<object> (null, o => (float?) 0)),
			};

		public MaxExpressionNode(MethodCallExpressionParseInfo parseInfo, LambdaExpression optionalSelector)
			: base(parseInfo, null, optionalSelector)
		{
		}

		public override Expression Resolve(
			ParameterExpression inputParameter, Expression expressionToBeResolved, ClauseGenerationContext clauseGenerationContext)
		{
			// no data streams out from this node, so we cannot resolve any expressions
			throw CreateResolveNotSupportedException();
		}

		protected override ResultOperatorBase CreateResultOperator(ClauseGenerationContext clauseGenerationContext)
		{
			return new MaxResultOperator();
		}
	}
}
