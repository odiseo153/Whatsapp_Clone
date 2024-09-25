
import Swal from "sweetalert2";

export class Message
{
    /* 
     * METHOD:
     * Static method of the class to display an error message
    */
    static errorMessage(message: string): void
    {
        const Toast = Swal.mixin({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            didOpen: (toast) => {
              toast.onmouseenter = Swal.stopTimer;
              toast.onmouseleave = Swal.resumeTimer;
            }
          });
          Toast.fire({
            icon: "error",
            title: `¡Oops! ${message}`
          });
    }

    /* 
     * METHOD:
     * Static method of the class to display an informative message
    */
    static informationMessage(message: string): void
    {
        const Toast = Swal.mixin({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            didOpen: (toast) => {
              toast.onmouseenter = Swal.stopTimer;
              toast.onmouseleave = Swal.resumeTimer;
            }
          });
          Toast.fire({
            icon: "info",
            title: `${message}`
          });
    }

    /* 
     * METHOD:
     * Static method of the class to display an successfull message
    */
    static successMessage(message: string): void
    {
        const Toast = Swal.mixin({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            didOpen: (toast) => {
              toast.onmouseenter = Swal.stopTimer;
              toast.onmouseleave = Swal.resumeTimer;
            }
          });
          Toast.fire({
            icon: "success",
            title: `${message}`
          });
    }

    /* 
     * METHOD:
     * Static method of the class to warning the user about lose all data
    */
    static async warningNormalMessage(message: string): Promise<boolean>
    {
      let boolAns = false;

      await Swal.fire(
          {
              title: "¡Espera!",
              text: message,
              icon: "warning",
              confirmButtonColor: "rgb(49, 111, 237)",
              confirmButtonText: "¡Sí!",
              showCancelButton: true,
              cancelButtonText: "No",
              allowOutsideClick: false
          }
      )
      .then((ans) => {
          if(ans.isConfirmed)
          {
              boolAns = true;
          }
      })

      return boolAns
    }
}