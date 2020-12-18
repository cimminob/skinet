import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

//uses the api errors controller for error testing

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  validationErrors: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  //get product that doesn't exist
  get404Error() {
    this.http.get(this.baseUrl + 'products/42').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  get500Error() {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  get400Error() {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  //happens when string is passed instead of number
  get400ValidationError() {
    this.http.get(this.baseUrl + 'products/fourtytwo').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
        this.validationErrors = error.errors;
      }
    );
  }
}
