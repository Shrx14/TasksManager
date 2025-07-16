using System;
using Microsoft.Data.SqlClient;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TasksManager.Middleware
{
    public class TcpErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TcpErrorHandlingMiddleware> _logger;

        public TcpErrorHandlingMiddleware(RequestDelegate next, ILogger<TcpErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SqlException ex) when (ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase) || ex.Message.Contains("connection", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError(ex, "SQL Connection Exception caught in middleware.");
                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(@"
                    <html>
                    <head>
                        <title>Connection Timeout Error</title>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                padding: 40px;
                                background-color: #b7e0fdff; /* Light blue background */
                                color: #333;
                                text-align: center;
                            }
                            h1 {
                                color: #2b2bb1ff;
                                font-size: 2.5rem;
                                margin-bottom: 20px;
                            }
                            p {
                                font-size: 1.2rem;
                                margin-bottom: 30px;
                            }
                            a.button {
                                display: inline-block;
                                padding: 12px 24px;
                                font-size: 1rem;
                                color: #fff;
                                background-color: #007bff;
                                border-radius: 5px;
                                text-decoration: none;
                                transition: background-color 0.3s ease;
                            }
                            a.button:hover {
                                background-color: #0056b3;
                            }
                        </style>
                    </head>
                    <body>
                        <h1>Connection Timeout Error</h1>
                        <p>Sorry, the server is currently unable to respond. Please reload the page.</p>
                        <a href='/' class='button'>Home</a>
                    </body>
                    </html>");
                return;
            }
            catch (SocketException ex)
            {
                _logger.LogError(ex, "Socket Exception caught in middleware.");
                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(@"
                    <html>
                    <head>
                        <title>Connection Timeout Error</title>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                padding: 40px;
                                background-color: #f8f9fa;
                                color: #333;
                                text-align: center;
                            }
                            h1 {
                                color: #dc3545;
                                font-size: 2.5rem;
                                margin-bottom: 20px;
                            }
                            p {
                                font-size: 1.2rem;
                                margin-bottom: 30px;
                            }
                            a.button {
                                display: inline-block;
                                padding: 12px 24px;
                                font-size: 1rem;
                                color: #fff;
                                background-color: #007bff;
                                border-radius: 5px;
                                text-decoration: none;
                                transition: background-color 0.3s ease;
                            }
                            a.button:hover {
                                background-color: #0056b3;
                            }
                        </style>
                    </head>
                    <body>
                        <h1>Connection Timeout Error</h1>
                        <p>Sorry, the server is currently unable to respond. Please try again later.</p>
                        <a href='/' class='button'>Home</a>
                    </body>
                    </html>");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught in middleware.");
                throw;
            }
        }
    }
}
