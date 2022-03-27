using Newtonsoft.Json;
using Ninject;
using System;
using System.IO;
using System.Text;

namespace MHW_Randomizer
{
    /// <summary>
    /// The IoC container for our application
    /// </summary>
    public static class IoC
    {
        #region Public Properties

        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static RandomizerViewModel Randomizer => IoC.Get<RandomizerViewModel>();
        public static RandomizerSettings Settings => IoC.Get<RandomizerSettings>();

        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC (Inversion of Control) container, binds all information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all 
        ///       services can be found
        /// </summary>
        public static void Setup()
        {
            // Bind all required view models
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModels()
        {
            // Bind to a single instance of Application view model
            RandomizerSettings userSettings = new RandomizerSettings();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Settings.json"))
            {
                using (StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "Settings.json"))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
                        DefaultValueHandling = DefaultValueHandling.Populate
                    };
                    userSettings = (RandomizerSettings)serializer.Deserialize(file, typeof(RandomizerSettings));
                }
            }
            Kernel.Bind<RandomizerSettings>().ToConstant(userSettings);
            Kernel.Bind<RandomizerViewModel>().ToConstant(new RandomizerViewModel());
        }

        #endregion

        /// <summary>
        /// Get's a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
