using ClinData.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ClinData.Application.DependencyInjection;
using ClinData.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// agregar servicios al contenedor.

builder.Services.AddControllers();
builder.Services.AddDbContext<ClinDataDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinData")));
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ClinData API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () =>
        Results.Content(
                """
                <!DOCTYPE html>
                <html lang="es">
                <head>
                    <meta charset="utf-8" />
                    <meta name="viewport" content="width=device-width, initial-scale=1" />
                    <title>ClinData API</title>
                    <style>
                        :root {
                            --bg: #eef3f7;
                            --card: #ffffff;
                            --ink: #1b2431;
                            --title: #0c3f69;
                            --accent: #1f7a68;
                            --muted: #5c6675;
                            --line: rgba(12, 63, 105, 0.16);
                            --shadow: 0 20px 45px rgba(9, 34, 61, 0.14);
                        }

                        * { box-sizing: border-box; }

                        body {
                            margin: 0;
                            min-height: 100vh;
                            display: grid;
                            place-items: center;
                            font-family: "Avenir Next", "Segoe UI", "Gill Sans", sans-serif;
                            background:
                                radial-gradient(850px 420px at 8% 0%, rgba(64, 132, 185, 0.16), transparent 68%),
                                radial-gradient(760px 380px at 100% 100%, rgba(31, 122, 104, 0.13), transparent 70%),
                                linear-gradient(145deg, #f5f9fc, var(--bg));
                            color: var(--ink);
                            padding: 1.2rem;
                        }

                        .card {
                            width: min(800px, 95vw);
                            background: var(--card);
                            border-radius: 20px;
                            box-shadow: var(--shadow);
                            padding: 0;
                            border: 1px solid var(--line);
                            overflow: hidden;
                        }

                        .hero {
                            padding: 1.7rem 2rem 1.25rem;
                            background: linear-gradient(135deg, #0c3f69 0%, #15598e 55%, #1f7a68 130%);
                            color: #f5fbff;
                        }

                        .tag {
                            display: inline-block;
                            font-size: 0.76rem;
                            letter-spacing: 1px;
                            text-transform: uppercase;
                            font-weight: 700;
                            padding: 0.32rem 0.62rem;
                            border: 1px solid rgba(255, 255, 255, 0.45);
                            border-radius: 999px;
                            margin-bottom: 0.9rem;
                        }

                        h1 {
                            margin: 0;
                            font-family: "Palatino Linotype", "Book Antiqua", Palatino, serif;
                            font-weight: 700;
                            font-size: clamp(1.9rem, 3vw, 2.6rem);
                            letter-spacing: 0.2px;
                        }

                        .subtitle {
                            margin: 0.45rem 0 0;
                            font-size: 1rem;
                            opacity: 0.95;
                        }

                        .content {
                            padding: 1.5rem 2rem 1.9rem;
                        }

                        .section-title {
                            font-size: 0.85rem;
                            font-weight: 700;
                            text-transform: uppercase;
                            letter-spacing: 1px;
                            color: var(--title);
                            margin: 1.1rem 0 0.55rem;
                        }

                        ul {
                            margin: 0;
                            padding-left: 1.2rem;
                        }

                        li {
                            margin: 0.38rem 0;
                        }

                        .teacher {
                            margin: 0;
                            font-family: "Palatino Linotype", "Book Antiqua", Palatino, serif;
                            font-size: 1.08rem;
                            color: #1f2f3d;
                        }

                        .quick-links {
                            margin: 0.15rem 0 0.4rem;
                            color: var(--muted);
                            font-size: 0.93rem;
                            line-height: 1.5;
                        }

                        .quick-links a {
                            color: var(--title);
                            text-decoration: none;
                            font-family: "SFMono-Regular", Menlo, Consolas, "Liberation Mono", monospace;
                            font-size: 0.9rem;
                        }

                        .quick-links a:hover {
                            text-decoration: underline;
                        }

                        .thanks {
                            margin-top: 1.4rem;
                            padding: 1rem 1.1rem;
                            border-left: 4px solid var(--accent);
                            background: #f4fbf8;
                            border-radius: 12px;
                            color: #13453d;
                            line-height: 1.5;
                        }

                        @media (max-width: 640px) {
                            .hero,
                            .content {
                                padding-left: 1.2rem;
                                padding-right: 1.2rem;
                            }
                        }
                    </style>
                </head>
                <body>
                    <main class="card">
                        <section class="hero">
                            <span class="tag">CEUTEC - Grupo #2</span>
                            <h1>API ClinData</h1>
                            <p class="subtitle">Proyecto academico - Sistema de gestion clinica</p>
                        </section>

                        <section class="content">
                            <p class="section-title">Probar API rapido</p>
                            <p class="quick-links">
                                <a href="/api/pacientes" target="_blank" rel="noopener noreferrer">/api/pacientes</a>
                                | <a href="/api/citas?fecha=2026-09-24" target="_blank" rel="noopener noreferrer">/api/citas</a>
                                | <a href="/api/notas/1" target="_blank" rel="noopener noreferrer">/api/notas</a>
                            </p>

                            <p class="section-title">Integrantes</p>
                            <ul>
                                <li>Milton Ortiz</li>
                                <li>Tania Oliva</li>
                                <li>Heber Pineda</li>
                            </ul>

                            <p class="section-title">Docente</p>
                            <p class="teacher">Ing. Maria J. Salinas</p>

                            <p class="thanks">Gracias por transmitir su conocimiento. Muy agradecidos, aprendimos mucho en su clase.</p>
                        </section>
                    </main>
                </body>
                </html>
                """,
                "text/html; charset=utf-8"));

app.MapControllers();

app.Run();