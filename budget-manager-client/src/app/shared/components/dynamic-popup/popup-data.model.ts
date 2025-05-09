export class PopupData {
  title?: string;
  message?: string;
  inputLabel?: string;
  showInput?: boolean;

  constructor(init?: Partial<PopupData>) {
    Object.assign(this, init);
  }
}
