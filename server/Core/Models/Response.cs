


namespace Whatsapp_Api.Core.Models
{
    public class Response
    {
        // Código de estado HTTP (por ejemplo, 200 para éxito, 400 para error de solicitud, etc.)
        public int StatusCode { get; set; }

        // Mensaje que puede describir el resultado de la operación (éxito, error, advertencia, etc.)
        public string Message { get; set; }

        // Indicador booleano para saber si la operación fue exitosa o fallida
        public bool IsSuccess { get; set; }

        // Los datos devueltos por la API (pueden ser de cualquier tipo T)
        public object Data { get; set; }

        // Constructor sin parámetros
        public Response() { }

        // Constructor para inicializar las propiedades
        public Response(int statusCode, string message, bool isSuccess, object data)
        {
            StatusCode = statusCode;
            Message = message; 
            IsSuccess = isSuccess;
            Data = data;
        }

        // Método estático para crear una respuesta exitosa
        public Response Success(object? data, string message = "Operation successful", int statusCode = 200)
        {
            return new Response(statusCode, message, true, data);
        }


        // Método estático para crear una respuesta de error
        public Response Error(object? message, int statusCode = 400)
        {
            string messageString;

            if (message is IEnumerable<object> enumerableMessage)
            {
                // Si el mensaje es una colección, conviértelo en una cadena
                messageString = string.Join(", ", enumerableMessage);
            }
            else
            {
                // Caso por defecto: conviértelo a string
                messageString = message?.ToString() ?? "Operation failed";
            }

            return new Response(statusCode, messageString, false, "Failed");
        }



    }

}
