export class PopupData {
  title?: string;
  message?: string;
  inputLabel?: string;
  inputFirst?: string;   
  inputSecond?: string;  
  inputThird?: string; 
  showInput?: boolean;
  buttonText?: string;
  cancelButtonText?: string;

  constructor(init?: Partial<PopupData>) {
    Object.assign(this, init);
  }
}
