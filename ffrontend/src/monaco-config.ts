import { NgZone } from '@angular/core';
import { editor } from 'monaco-editor';

let zone: NgZone;

export function initMonaco(ngZone: NgZone) {
  zone = ngZone;
  
  editor.onDidCreateEditor(() => {
    zone.run(() => {});
  });
}