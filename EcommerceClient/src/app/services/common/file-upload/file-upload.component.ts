import { Component, Input } from '@angular/core';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent {

  constructor(private httpClientService: HttpClientService, private alertify:AlertifyService, private customToastrService: CustomToastrService) { }

  public files: NgxFileDropEntry[];

  @Input() options: Partial<FileUploadOptions>;  

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    for(const file of files){
      (file.fileEntry as FileSystemFileEntry).file((_file:File)=>{
        fileData.append(_file.name, _file,file.relativePath);
      });
    }

    this.httpClientService.post({
      controller:this.options.controller,
      action:this.options.action,
      queryString:this.options.queryString,
      headers: new HttpHeaders({"responseType":"blob"})
    }, fileData).subscribe(data=>{

      const message: string = "Files succesfully uploaded";

      if(this.options.isAdminPage){
        this.alertify.message(message,{
          dismissOthers:true,
          messageType:MessageType.Success,
          position:Position.TopRight
        })
      } else{
         this.customToastrService.message(message, 'Succesful',{
          messageType:ToastrMessageType.Success,
          position:ToastrPosition.TopRight
         });
      }

    },(errorResponse:HttpErrorResponse)=>{

      const message: string = "Something went wrong when files uploaded";

      if(this.options.isAdminPage){
        this.alertify.message(message,{
          dismissOthers:true,
          messageType:MessageType.Error,
          position:Position.TopRight
        })
      } else{
         this.customToastrService.message(message, 'Failed',{
          messageType:ToastrMessageType.Error,
          position:ToastrPosition.TopRight
         });
      }

    });

  }
}

export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}