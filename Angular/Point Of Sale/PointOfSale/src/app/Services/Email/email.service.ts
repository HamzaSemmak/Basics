import { Injectable } from '@angular/core';
import emailjs, { EmailJSResponseStatus } from 'emailjs-com';
import { EMAIL_SERVICE_ID, EMAIL_TEMPLATE_ID, EMAIL_USER_ID } from 'src/app/Modules/Config/Config';

@Injectable({
  providedIn: 'root'
})
export class EmailService {

  constructor() { }

  sendEmail(To: string, Code: string, Name: string) {
    const Params = {
      to_email: To,
      message: Code,
      name: Name
    };
  
    emailjs.send(EMAIL_SERVICE_ID, EMAIL_TEMPLATE_ID, Params, EMAIL_USER_ID)
      .then((response: EmailJSResponseStatus) => {
        console.log(response);
      }, (error) => {
        console.log(error);
      });
  }
}
