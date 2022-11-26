using System.Reflection;

namespace Services.Factories
{
    internal sealed class ConstructorDIController : DIController
    {
        public ConstructorDIController(ConstructorInfo ctor) : base(
            ctor.GetParameters()
            .Select(ParameterInfoToParameterType))
        {
        }

        private static Type ParameterInfoToParameterType(ParameterInfo parameter) =>
            parameter.ParameterType;
    }
}