import { Component } from '@angular/core';
import { ItemDetailsResult } from 'src/app/shared/models/items';
import { ChatbotService } from '../chatbot.service';

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.scss']
})
export class ChatbotComponent {
  keyword: string = ''; // Variable to store the keyword entered by the user
  items: ItemDetailsResult | null = null; // Variable to store search results
  errorMessage: string | null = null; // Variable to store any error message

  constructor(private chatbotService: ChatbotService) { }

  searchItems() {
    if (this.keyword.trim()) {
      this.chatbotService.getItemsByKeyword(this.keyword).subscribe({
        next: (result) => {
          this.items = result;
          this.errorMessage = null;
        },
        error: (error) => {
          this.errorMessage = 'An error occurred while fetching items.';
          this.items = null;
          console.error('Error:', error);
        }
      });
    } else {
      this.errorMessage = 'Please enter a keyword to search.';
      this.items = null;
    }
  }
}
