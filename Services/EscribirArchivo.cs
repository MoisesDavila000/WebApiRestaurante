namespace WebApiRestaurante2.Services
{
    public class EscribirArchivo :IHostedService
    {
        private readonly IWebHostEnvironment env;

        private readonly string nombreArchivo = "Restaurante.txt";

        private Timer timer;

        public EscribirArchivo (IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Escribir("Se inicio la aplicacion.");
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Escribir("Se cerro la aplicacion.");
            timer.Dispose();
            
            return Task.CompletedTask;
        }

        public void PetGet()
        {
            Escribir("Se realizo una peticion GET. " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        public void PetPost()
        {
            Escribir("Se realizo una peticion Post. " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        public void PetPut()
        {
            Escribir("Se realizo una peticion Put. " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        public void PetDelete()
        {
            Escribir("Se realizo una peticion Delete. " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private void DoWork(object state)
        {
            Escribir("Proceso en ejecucion: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";

            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }
    }
}
