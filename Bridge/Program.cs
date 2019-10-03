using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    class VectorRenderer : IRenderer
    {

        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();

            cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();

            cb.Register((c, p) =>
                new Circle(c.Resolve<IRenderer>(),
                    p.Positional<float>(0)));

            
            using (var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));

                circle.Draw();
                circle.Resize(5);
                circle.Draw();
            }

            foreach (var cbProperty in cb.Properties)
            {
                Console.WriteLine(cbProperty.Value + " " + cbProperty.Key);
            }

            ////var renderer = new RasterRenderer(); 
            //var renderer = new VectorRenderer();
            //var circle = new Circle(renderer, 5);

            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();
        }
    }
}
