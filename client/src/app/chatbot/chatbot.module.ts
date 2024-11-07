import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatbotComponent } from './chatbot/chatbot.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    ChatbotComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    TableModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
   
  ]
})
export class ChatbotModule { }
