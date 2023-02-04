using static ExtensionsFluentBranches;

public static class ExtensionsFluentBranches
{
    //public static FluentBranchBuilderWhere<T> Branch<T>(this IEnumerable<T> collection) => new FluentBranchBuilderWhere<T>(collection);


    public record FluentBranchBuilderWhere<T>(IEnumerable<T> collection)
    {
        protected Dictionary<Func<T, bool>, Action<T>> IfExpressionsDictionary = new();

        protected List<Func<T, bool>> IfExpressionsList = new();

        protected Action<T> ElseAction;

        public FluentBranchBuilderWhere<T> If(Func<T, bool> IfExpression)
        {
            IfExpressionsList.Add(IfExpression);
            return this;
        }


        public FluentBranchBuilderWhere<T> If(Func<T, bool> IfExpression, Action<T> action)
        {
            IfExpressionsDictionary.Add(IfExpression, action);
            return this;
        }
        public FluentBranchBuilderWhere<T> Else(Action<T> ElseAction)
        {
            this.ElseAction = ElseAction;
            return this;
        }




        public IEnumerable<T> Collect()
        {
            foreach (var e in collection)
            {
                if (e is null) continue;

                foreach (var IfExpression in IfExpressionsList)
                {
                    if (IfExpression(e))
                    {
                        yield return e;
                        break;
                    }
                }
                foreach (var IfExpressionPair in IfExpressionsDictionary)
                {
                    if (IfExpressionPair.Key(e))
                    {
                        IfExpressionPair.Value(e);
                        yield return e;
                        break;
                    }
                }

                if (ElseAction != null)
                {
                    ElseAction?.Invoke(e);
                    yield return e;
                }
            }
        }

    }



    public static FluentBranchIEnumerable<T> If<T>(this IEnumerable<T> collection, Func<T, bool> IfExpression)
    {
        var BranchIEnumerable = new FluentBranchIEnumerable<T>(collection);
        BranchIEnumerable.If(IfExpression);
        return BranchIEnumerable;
    }

    public static FluentBranchIEnumerable<T> If<T>(this IEnumerable<T> collection, 
        Func<T, bool> IfExpression, Action<T> action)
    {
        var BranchIEnumerable = new FluentBranchIEnumerable<T>(collection);
        BranchIEnumerable.If(IfExpression, action);
        return BranchIEnumerable;
    }

    public class FluentBranchIEnumerable<T> : IEnumerable<T>
    {
        IEnumerable<T> collection;
        public FluentBranchIEnumerable(IEnumerable<T> collection)
        {
            this.collection = collection;
        }


        protected Dictionary<Func<T, bool>, Action<T>> IfExpressionsDictionary = new();

        protected HashSet<Func<T, bool>> IfExpressionsList = new();

        protected Action<T> ElseAction;

        public FluentBranchIEnumerable<T> If(Func<T, bool> IfExpression)
        {
            IfExpressionsList.Add(IfExpression);
            return this;
        }


        public FluentBranchIEnumerable<T> If(Func<T, bool> IfExpression, Action<T> action)
        {
            IfExpressionsDictionary.Add(IfExpression, action);
            return this;
        }
        public FluentBranchIEnumerable<T> Else(Action<T> ElseAction)
        {
            this.ElseAction = ElseAction;
            return this;
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var e in collection)
            {
                if (e is null) continue;

                foreach (var IfExpression in IfExpressionsList)
                {
                    if (IfExpression(e))
                    {
                        yield return e;
                        break;
                    }
                }
                foreach (var IfExpressionPair in IfExpressionsDictionary)
                {
                    if (IfExpressionPair.Key(e))
                    {
                        IfExpressionPair.Value(e);
                        yield return e;
                        break;
                    }
                }

                if (ElseAction != null)
                {
                    ElseAction?.Invoke(e);
                    yield return e;
                }
            }
        }

    }

}