using Backend.Core.Interfaces;
using Backend.Core.InterfacesImpl;
using Ninject;

namespace Backend.Core.Injections
{
    public class CoreKernel
    {
        static IKernel _iKernel = new StandardKernel();
        static CoreKernel _apiKernel;

        private CoreKernel()
        {
            _iKernel.Bind<IAsteroids>().To<Asteroids>();
        }

        public static CoreKernel retrieve()
        {
            lock (_iKernel)
            {
                if (_apiKernel == null)
                {
                    _apiKernel = new CoreKernel();
                }
            }

            return _apiKernel;
        }

        public IAsteroids getAsteroids()
        {
            return _iKernel.Get<IAsteroids>();
        }

    }
}
