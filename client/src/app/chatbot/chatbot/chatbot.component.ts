import { Component, ChangeDetectorRef } from '@angular/core';
import { ItemDetailsResult } from 'src/app/shared/models/items';
import { ChatbotService } from '../chatbot.service';

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.scss']
})
export class ChatbotComponent {
  keyword: string = '';
  items: ItemDetailsResult | null = null;
  errorMessage: string | null = null;

  constructor(
    private chatbotService: ChatbotService,
    private cdr: ChangeDetectorRef
  ) {}

  searchItems() {
    if (this.keyword.trim()) {
      this.chatbotService.getItemsByKeyword(this.keyword).subscribe({
        next: (result) => {
          this.items = result;  // result will be of type ItemDetailsResult
          console.log('API result:', result);
          this.errorMessage = null;
          this.cdr.detectChanges(); // Trigger change detection
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
