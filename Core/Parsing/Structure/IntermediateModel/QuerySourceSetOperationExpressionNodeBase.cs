using System;
using System.Linq.Expressions;
using Remotion.Linq.Clauses;
using Remotion.Linq.Utilities;

namespace Remotion.Linq.Parsing.Structure.IntermediateModel
{
	/// <summary>
	/// Acts as a base class for <see cref="UnionExpressionNode"/> and <see cref="ConcatExpressionNode"/>, i.e., for node parsers for set operations
	/// acting as an <see cref="IQuerySource"/>.
	/// </summary>
	public abstract class QuerySourceSetOperationExpressionNodeBase : ResultOperatorExpressionNodeBase, IQuerySourceExpressionNode
	{
		protected QuerySourceSetOperationExpressionNodeBase(MethodCallExpressionParseInfo parseInfo, Expression source2)
			: base(parseInfo, null, null)
		{
			Source2 = source2;
			ItemType = ReflectionUtility.GetItemTypeOfClosedGenericIEnumerable(parseInfo.ParsedExpression.Type, "expression");
		}

		public Expression Source2 { get; private set; }
		public Type ItemType { get; private set; }

		public override Expression Resolve(ParameterExpression inputParameter, Expression expressionToBeResolved, ClauseGenerationContext clauseGenerationContext)
		{
			// UnionResultOperator is a query source, so expressions resolve their input parameter with the UnionResultOperator created by this node.
			return QuerySourceExpressionNodeUtility.ReplaceParameterWithReference(this, inputParameter, expressionToBeResolved, clauseGenerationContext);
		}

		protected abstract ResultOperatorBase CreateSpecificResultOperator();

		protected sealed override ResultOperatorBase CreateResultOperator(ClauseGenerationContext clauseGenerationContext)
		{
			var resultOperator = CreateSpecificResultOperator();
			clauseGenerationContext.AddContextInfo(this, resultOperator);
			return resultOperator;
		}
	}
}