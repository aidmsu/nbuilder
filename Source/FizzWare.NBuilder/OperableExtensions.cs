using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public static class OperableExtensions
    {
        private static IDeclaration<T> GetDeclaration<T>(IOperable<T> operable)
        {
            var declaration = operable as IDeclaration<T>;

            if (declaration == null)
                throw new ArgumentException("Must be of type IDeclaration<T>");

            return declaration;
        }

        public static IOperable<T> Have<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            var declaration = GetDeclaration(operable);

            declaration.ObjectBuilder.With(func);
            return (IOperable<T>)declaration;
        }

        public static IOperable<T> And<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            return Have(operable, func);
        }

        public static IOperable<T> And<T>(this IOperable<T> operable, Action<T> action)
        {
            return HaveDoneToThem(operable, action);
        }

        public static IOperable<T> AreConstructedWith<T>(this IOperable<T> operable, params object[] args)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.WithConstructorArgs(args);
            return (IOperable<T>)declaration;
        }

        public static IOperable<T> HaveDoneToThem<T>(this IOperable<T> operable, Action<T> action)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.Do(action);
            return (IOperable<T>)declaration;
        }

        public static IOperable<T> HaveDoneToThemForAll<T, U>(this IOperable<T> operable, Action<T, U> action, IList<U> list)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.DoMultiple(action, list);
            return (IOperable<T>)declaration;
        }
    }
}