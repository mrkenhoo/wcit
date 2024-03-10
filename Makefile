SOLUTION_FILE     = "Windows Installer.sln"
EXTRA_FLAGS       = --nologo
RUNTIME           = win-x64
DEBUG_CONFIG      = --configuration Debug
RELEASE_CONFIG    = --configuration Release
BUILD_FLAGS       = --self-contained --runtime $(RUNTIME)
BUILD_EXTRA_FLAGS = --configuration Release

all:
	@echo "Valid arguments are: test, build, run or publish"

test:
	dotnet test $(EXTRA_FLAGS)

build:
	@dotnet build $(SOLUTION_FILE) $(EXTRA_FLAGS) $(BUILD_FLAGS) $(BUILD_EXTRA_FLAGS)

run:
	@dotnet run $(SOLUTION_FILE) $(EXTRA_FLAGS) $(BUILD_FLAGS) $(BUILD_EXTRA_FLAGS)

publish:
	@dotnet publish $(SOLUTION_FILE) $(EXTRA_FLAGS) $(BUILD_FLAGS) $(BUILD_EXTRA_FLAGS)

