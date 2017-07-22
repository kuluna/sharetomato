import { SharetomatoPage } from './app.po';

describe('sharetomato App', () => {
  let page: SharetomatoPage;

  beforeEach(() => {
    page = new SharetomatoPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
