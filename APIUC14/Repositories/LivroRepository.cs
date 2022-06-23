using APIUC14.Contexts;
using APIUC14.Models;

namespace APIUC14.Repositories
{
        public class LivroRepository
        {
            private readonly ChapterContext _context;
            public LivroRepository(ChapterContext context)
            {
                _context = context;
            }

            public List<Livro> Listar()
            {
                return _context.Livros.ToList();
            }

            public Livro BuscarPorId(int id)
            {
                return _context.Livros.Find(id);
            }

            public void Cadastrar(Livro Livro)
            {
                _context.Livros.Add(Livro); //insert no BD

                _context.SaveChanges();
            }

            public void Atualizar(int id, Livro livro)
            {
                Livro livroBuscado = _context.Livros.Find(id);

                if (livroBuscado != null)
                {
                    livroBuscado.Titulo = livro.Titulo;

                    livroBuscado.Disponivel = livro.Disponivel;

                    livroBuscado.QuantidadePaginas = livro.QuantidadePaginas;
                }

                _context.Livros.Update(livroBuscado);
                _context.SaveChanges();
            }

            public void Deletar(int id)
            {
                Livro livro = _context.Livros.Find(id);

                _context.Livros.Remove(livro);

                _context.SaveChanges();
            }
        }
}
