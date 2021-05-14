using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RockPaperScissors.Factory
{
    public class StrategyFactory<InterfaceType>
    {
        private List<Type> _strategyTypes = new List<Type>();

        public StrategyFactory()
        {
            var targetInterface = typeof(InterfaceType);

            var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory,"Strategies"), "*.dll");

            foreach (var dll in files)
            {
                var assm = Assembly.LoadFrom(dll);

                var stratTypes = assm.GetTypes().Where(t => !t.IsAbstract && targetInterface.IsAssignableFrom(t));

                _strategyTypes.AddRange(stratTypes);
            }
        }

        public string Options
        {
            get
            {
                StringBuilder bob = new StringBuilder();

                foreach (var strat in _strategyTypes)
                {
                    bob.Append(strat.Name);

                    if (strat != _strategyTypes.Last())
                        bob.Append(", ");
                }

                return bob.ToString();
            }
        }

        public InterfaceType Create(string name)
        {
            var type = _strategyTypes.Single(t => t.Name == name);

            return (InterfaceType)Activator.CreateInstance(type);
        }
    }
}
