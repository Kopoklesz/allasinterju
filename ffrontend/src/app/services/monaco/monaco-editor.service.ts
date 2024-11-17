import { Injectable, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject } from 'rxjs';
import type { editor } from 'monaco-editor';

@Injectable({
  providedIn: 'root'
})
export class MonacoEditorService {
  private monacoLoaded = new BehaviorSubject<boolean>(false);
  monacoLoaded$ = this.monacoLoaded.asObservable();
  private monaco: any;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    if (isPlatformBrowser(this.platformId)) {
      this.initMonaco();
    }
  }

  private async initMonaco() {
    try {
      if (typeof window !== 'undefined') {
        this.monaco = await import('monaco-editor');
        this.monacoLoaded.next(true);
      }
    } catch (error) {
      console.error('Failed to load Monaco Editor:', error);
    }
  }

  createEditor(container: HTMLElement, options: editor.IStandaloneEditorConstructionOptions): editor.IStandaloneCodeEditor | null {
    if (!this.monaco || !isPlatformBrowser(this.platformId)) {
      return null;
    }

    return this.monaco.editor.create(container, {
      ...options,
      theme: 'vs-dark',
      automaticLayout: true
    });
  }

  setModelLanguage(model: editor.ITextModel, language: string) {
    if (!this.monaco || !isPlatformBrowser(this.platformId)) {
      return;
    }

    this.monaco.editor.setModelLanguage(model, language);
  }
}