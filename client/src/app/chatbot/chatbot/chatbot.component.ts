import { Component, ChangeDetectorRef } from '@angular/core';
import { ItemDetailsDto } from 'src/app/shared/models/items';
import { ChatbotService } from '../chatbot.service';

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.scss']
})
export class ChatbotComponent {
  keyword: string = '';
  messages: { text: string; sender: 'user' | 'bot'; type?: 'text' | 'results'; items?: ItemDetailsDto[] }[] = [];
  errorMessage: string | null = null;

  constructor(
    private chatbotService: ChatbotService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    // Initial greeting from the chatbot
    this.messages.push({ text: 'مرحبًا! كيف يمكنني مساعدتك اليوم؟ أخبرني باسم العنصر الذي تبحث عنه؟', sender: 'bot', type: 'text' });
  }

  sendMessage() {
    if (this.keyword.trim()) {
      // Add the user's message
      this.messages.push({ text: this.keyword, sender: 'user', type: 'text' });

      // Call search method for the keyword
      this.searchItems(this.keyword);

      // Clear input field
      this.keyword = '';
    } else {
      this.errorMessage = 'Please enter a keyword to search.';
    }
  }

  searchItems(keyword: string) {
    this.chatbotService.getItemsByKeyword(keyword).subscribe({
      next: (result: ItemDetailsDto[]) => {
        // Add bot's response with search results
        this.messages.push({ text: `Results for "${keyword}"`, sender: 'bot', type: 'results', items: result });
        this.errorMessage = null;
        this.cdr.detectChanges();
      },
      error: (error) => {
        // Add error message as bot response
        this.messages.push({ text: 'An error occurred while fetching items.', sender: 'bot', type: 'text' });
        this.errorMessage = 'An error occurred while fetching items.';
        console.error('Error:', error);
      }
    });
  }
}
