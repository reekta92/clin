#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_NOTE_LENGTH 256
#define NOTES_FILE "notes.txt"

void clear_notes() {
    FILE *file = fopen(NOTES_FILE, "w");
    if (file) {
        fclose(file);
        printf("\033[1;33mNotes cleared successfully!\033[0m\n");
    }
}

void show_notes() {
    FILE *file = fopen(NOTES_FILE, "r");
	if (!file || fgetc(file) == EOF) {
		printf("\033[1;31mNo notes found!\033[0m\n");
		if (file) fclose(file);
		return;
	}
	rewind(file);

    printf("\033[1;34mNotes:\033[0m\n");
    char note[MAX_NOTE_LENGTH];
    while (fgets(note, sizeof(note), file)) {
        printf("%s", note);
    }
    fclose(file);
}

void add_note(const char *note) {
    FILE *file = fopen(NOTES_FILE, "a");
    if (file) {
        fprintf(file, "%s\n", note);
        fclose(file);
        printf("\033[1;33mNote added successfully!\033[0m\n");
    }
}

int main() {
    printf("\033[1;34mPlease enter your notes. Type 'notes' to open the notes "
           "file, 'clear' to clear the notes, 'show' to show the notes, or "
           "'exit' to exit the program.\033[0m\n");

    char userInput[MAX_NOTE_LENGTH];

    while (1) {
        printf("\033[1;32m> \033[0m");
        if (!fgets(userInput, sizeof(userInput), stdin)) {
            continue;
        }

        // Remove newline character
        userInput[strcspn(userInput, "\n")] = 0;

        if (strcmp(userInput, "exit") == 0) {
            exit(0);
        } else if (strcmp(userInput, "notes") == 0) {
            system("notepad " NOTES_FILE);
        } else if (strcmp(userInput, "clear") == 0) {
            clear_notes();
        } else if (strcmp(userInput, "show") == 0) {
            show_notes();
        } else {
            add_note(userInput);
        }
    }

    return 0;
}
