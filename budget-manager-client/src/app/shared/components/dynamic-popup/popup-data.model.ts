export class PopupData {
  title?: string;
  message?: string;
  inputLabel?: string;
  showInput?: boolean;
  buttonText?: string;
  cancelButtonText?: string;

  constructor(init?: Partial<PopupData>) {
    Object.assign(this, init);
  }
}
