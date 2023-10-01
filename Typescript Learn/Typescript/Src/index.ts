import * as fs from 'fs';

function countWords(content: string): number {
  const words = content.split(/\s+/);
  return words.length;
}

function readTextFile(filePath: string): string {
  return fs.readFileSync(filePath, 'utf-8');
}

function displayWordCount(wordCount: number): void {
  console.log(`Total words: ${wordCount}`);
}

function main() {
  const filePath = process.argv[2];
  
  if (!filePath) {
    console.log('Please provide a file path.');
    return;
  }

  try {
    const content = readTextFile(filePath);
    const wordCount = countWords(content);
    displayWordCount(wordCount);
  } catch (error) {
    console.log('An error occurred:' + error);
  }
}

main();
