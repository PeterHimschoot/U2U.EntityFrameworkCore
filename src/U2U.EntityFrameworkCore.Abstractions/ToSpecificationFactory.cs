﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace U2U.EntityFrameworkCore
{
#nullable enable
  public class ToSpecificationFactory
  {
    internal class CreateInfo<T>
      where T : new()
    {
      internal T Instance { get; set; } = new T();
    }

    internal class SpecificationVisitor<T> : ExpressionVisitor
      where T : new()
    {
      private readonly ChainOfResponsibility<CreateInfo<T>> creationCOR;

      // private T Parameter { get; set; }

      public SpecificationVisitor(ChainOfResponsibility<CreateInfo<T>> creationCOR)
      {
        this.creationCOR = creationCOR;
      }

      private (MemberInfo?, object?) TryGetPropertyNameAndValue(BinaryExpression node)
      {
        var (propertyName, value) = TryGetPropertyNameAndValue(node.Left, node.Right);
        if (propertyName == null)
        {
          (propertyName, value) = TryGetPropertyNameAndValue(node.Right, node.Left);
        }
        return (propertyName, value);
      }

      private (MemberInfo?, object?) TryGetPropertyNameAndValue(Expression left, Expression right)
      {
        MemberInfo? property = null;
        object? value = null;
        if (left is MemberExpression)
        {
          MemberExpression me = (left as MemberExpression)!;
          MemberInfo mi = me.Member;
          if (mi.DeclaringType == typeof(T))
          {
            property = mi;
            value = GetValue(right);
          }
        }
        return (property, value);
      }

      private object? GetValue(Expression right)
      {
        var f = Expression.Lambda(right).Compile();
        return f.DynamicInvoke();
      }

      protected override Expression VisitParameter(ParameterExpression node)
      {
        //this.Parameter = node.
        return base.VisitParameter(node);
      }

      protected override Expression VisitConditional(ConditionalExpression node)
        => base.VisitConditional(node);

      protected override Expression VisitMember(MemberExpression node)
        => base.VisitMember(node);

      protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        => base.VisitMemberAssignment(node);

      protected override MemberBinding VisitMemberBinding(MemberBinding node)
        => base.VisitMemberBinding(node);

      protected override Expression VisitMethodCall(MethodCallExpression node)
        => base.VisitMethodCall(node);

      protected override Expression VisitBinary(BinaryExpression node)
      {
        switch (node.NodeType)
        {
          case ExpressionType.And:
          case ExpressionType.AndAlso:
          case ExpressionType.Or:
          case ExpressionType.OrElse:
            //this.VisitBinary((BinaryExpression) node.Left);
            //this.VisitBinary((BinaryExpression) node.Right);
            break;
          case ExpressionType.Equal:
            var (prop, value) = TryGetPropertyNameAndValue(node);
            creationCOR.Add((next, t) =>
            {
              PropertyInfo pi = (prop as PropertyInfo)!;
              pi.SetValue(t.Instance, value);
              next(t);
            });
            break;
          case ExpressionType.NotEqual:
            (prop, value) = TryGetPropertyNameAndValue(node);
            if (prop == null)
            {
              break;
            }
            value = InverseValue((prop as PropertyInfo)!.PropertyType, value);
            creationCOR.Add((next, t) =>
            {
              PropertyInfo pi = (prop as PropertyInfo)!;
              pi.SetValue(t.Instance, value);
              next(t);
            });
            break;

        }
        return base.VisitBinary(node);
      }

      private static Dictionary<Type, Func<object?, object?>> inverters = new Dictionary<Type, Func<object?, object?>>
      {
        { typeof(int), (object? i) => ((int) i!) == 0 ? 1 : 0 },
        { typeof(string), (object? s) => s == null ? string.Empty : null }
      };

      private object? InverseValue(Type type, object? value)
        => inverters[type](value);
    }

    public static T? CreateFromSpecification<T>(Specification<T> specification)
      where T : class, new()
    {
      var creationCOR = new ChainOfResponsibility<CreateInfo<T>>(t => { });
      var visitor = new SpecificationVisitor<T>(creationCOR);
      visitor.Visit(specification.Criteria);

      var processor = creationCOR.BuildProcessor();
      var createInfo = new CreateInfo<T>();
      processor(createInfo);
      return createInfo.Instance;
    }
  }
}
