using Services.Factories;

namespace Services
{
    internal class DefaultDependencyInjectionRules : IDependencyInjectionRules
    {
        public DefaultDependencyInjectionRules(int maxDependencyInjectionDepth)
        {
            MaxDependencyInjectionDepth = maxDependencyInjectionDepth;
        }

        public DefaultDependencyInjectionRules() : this(32)
        {
        }

        public int MaxDependencyInjectionDepth { get; set; }

        public bool ControllerMapIsCorrect(IDictionary<Type, IDIController> controllerMap)
        {
            CancellationTokenSource cts = new();
            foreach (var controller in controllerMap.Values)
            {
                HashSet<Type> dependencyChain = new();

                if (IsInfinite(controllerMap, controller, dependencyChain, 1, cts))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsInfinite(IDictionary<Type, IDIController> controllerMap, IDIController controller, HashSet<Type> dependencyChain, int currentDIDepth, CancellationTokenSource cts)
        {
            if (currentDIDepth > MaxDependencyInjectionDepth)
            {
                cts.Cancel();
                return true;
            }
            if (cts.IsCancellationRequested)
            {
                return false;
            }
            int previousCount = dependencyChain.Count;
            foreach (Type currentDependencyType in controllerMap.Keys)
            {
                if (controller.Contains(currentDependencyType))
                {
                    var currentSet = dependencyChain.ToHashSet();
                    currentSet.Add(currentDependencyType);

                    if (previousCount == currentSet.Count) // type not added
                    {
                        cts.Cancel();
                        return true;
                    }

                    if (controllerMap.TryGetValue(currentDependencyType, out var dependencyController) &&
                        IsInfinite(controllerMap, dependencyController, currentSet, currentDIDepth + 1, cts))
                    {
                        return true;
                    }

                    if (cts.IsCancellationRequested)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}