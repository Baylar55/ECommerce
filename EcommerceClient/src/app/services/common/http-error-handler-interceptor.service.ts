import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor{

  constructor(private toastrService:CustomToastrService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error=>{
      switch(error.status){
        case HttpStatusCode.Unauthorized:
          this.toastrService.message("You don't have permission to do this operation!", "Unauthorized access!",{
            messageType:ToastrMessageType.Warning,
            position:ToastrPosition.TopRight
          });
          break;
        case HttpStatusCode.InternalServerError:
          this.toastrService.message("Unable to reach server!", "Server error!",{
            messageType:ToastrMessageType.Warning,
            position:ToastrPosition.TopRight
          });
          break;
        case HttpStatusCode.BadRequest:
          this.toastrService.message("Invalid request made!", "Invalid request!",{
            messageType:ToastrMessageType.Warning,
            position:ToastrPosition.TopRight
          });
          break;
        case HttpStatusCode.NotFound:
          this.toastrService.message("Page not found!", "Not Found!",{
            messageType:ToastrMessageType.Warning,
            position:ToastrPosition.TopRight
          });
          break;
        default:
          this.toastrService.message("An unexpected error occured!", "Unexpected error!",{
            messageType:ToastrMessageType.Warning,
            position:ToastrPosition.TopRight
          }); 
          break;
      }

      return of(error)
    }));
  }
}
