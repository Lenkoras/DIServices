using Services.Factories;

namespace Services
{
    /// <summary>
    /// Dependency injection rules.
    /// </summary>
    public interface IDependencyInjectionRules
    {
        /// <summary>
        /// Maximum allowable dependency injection depth.
        /// </summary>
        int MaxDependencyInjectionDepth { get; set; }

        /// <summary>
        /// Checks if a <paramref name="controllerMap"/> passes the sets rules.
        /// </summary>
        /// <param name="controllerMap">Controllers key value map.</param>
        /// <returns>The value indicate controllers map passes the sets rules or not.</returns>
        bool ControllerMapIsCorrect(IDictionary<Type, IDIController> controllerMap);
    }
}